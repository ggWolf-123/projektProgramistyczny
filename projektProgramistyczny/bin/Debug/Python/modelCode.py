import os
import sys
from PIL import Image
import torch
import torch.nn as nn
import torchvision.transforms as transforms
class UNet(nn.Module):
    def __init__(self):
        super(UNet, self).__init__()
        
        def conv_block(in_channels, out_channels):
            return nn.Sequential(
                nn.Conv2d(in_channels, out_channels, kernel_size=3, padding=1),
                nn.BatchNorm2d(out_channels),
                nn.ReLU(inplace=True),
                nn.Conv2d(out_channels, out_channels, kernel_size=3, padding=1),
                nn.BatchNorm2d(out_channels),
                nn.ReLU(inplace=True)
            )

        self.enc1 = conv_block(1, 64)
        self.pool1 = nn.MaxPool2d(2)
        self.enc2 = conv_block(64, 128)
        self.pool2 = nn.MaxPool2d(2)
        self.enc3 = conv_block(128, 256)
        self.pool3 = nn.MaxPool2d(2)
        self.enc4 = conv_block(256, 512)
        self.pool4 = nn.MaxPool2d(2)

        self.bottleneck = conv_block(512, 1024)

        self.up4 = nn.ConvTranspose2d(1024, 512, kernel_size=2, stride=2)
        self.dec4 = conv_block(1024, 512)
        self.up3 = nn.ConvTranspose2d(512, 256, kernel_size=2, stride=2)
        self.dec3 = conv_block(512, 256)
        self.up2 = nn.ConvTranspose2d(256, 128, kernel_size=2, stride=2)
        self.dec2 = conv_block(256, 128)
        self.up1 = nn.ConvTranspose2d(128, 64, kernel_size=2, stride=2)
        self.dec1 = conv_block(128, 64)

        self.final = nn.Conv2d(64, 3, kernel_size=1)
        self.activation = nn.Tanh()

    def forward(self, x):
        e1 = self.enc1(x)
        e2 = self.enc2(self.pool1(e1))
        e3 = self.enc3(self.pool2(e2))
        e4 = self.enc4(self.pool3(e3))

        b = self.bottleneck(self.pool4(e4))

        d4 = self.dec4(torch.cat([self.up4(b), e4], dim=1))
        d3 = self.dec3(torch.cat([self.up3(d4), e3], dim=1))
        d2 = self.dec2(torch.cat([self.up2(d3), e2], dim=1))
        d1 = self.dec1(torch.cat([self.up1(d2), e1], dim=1))

        out = self.final(d1)
        return self.activation(out)

# sprawdzanie argumętów
if len(sys.argv)<4:
    print("Użycie; python modelCode.py <ścieżka_wejściowa> <ścieżka_wyjściowa> <wersja>",file=sys.stderr)
    sys.exit(1)

input_path=sys.argv[1]
output_path=sys.argv[2]
version=sys.argv[3]
try:
    version=int(version)
except ValueError:
    print(f"Bład: wersja '{version}' nie jest liczbą całkowitą.",file=sys.stderr)
    sys.exit(1)
if not os.path.exists(input_path):
    print(f"Błąd; Plik {input_path} nie istnieje.",file=sys.stderr)
    sys.exit(1)

script_dir=os.path.dirname(os.path.abspath(__file__))
model_filename=f"model_v{version}.pth"
model_path=os.path.join(script_dir,model_filename)
if not os.path.exists(model_path):
    print(f"Błąd: Model {model_path} nie istnieje.",file=sys.stderr)
    sys.exit(1)
    
device=torch.device("cuda" if torch.cuda.is_available() else "cpu")

# wczytanie modelu
model=UNet().to(device)
model.load_state_dict(torch.load(model_path,map_location=device))
model.eval()

# wczytanie obrazu
try:
    img=Image.open(input_path).convert("L")
except Exception as e:
    print(f"Bład podczas wczytywania obrazu: {str(e)}",file=sys.stderr)
    sys.exit(1)

original_size=img.size
# zachowanie proporcji
w,h=img.size
scale=128/max(w,h)
new_w=int(w*scale)
new_h=int(h*scale)
img_resized=img.resize((new_w,new_h),Image.BICUBIC)
#padding do wielokrotności 16
pad_w=(16-new_w%16)%16
pad_h=(16-new_h%16)%16
padding=(0,0,pad_w,pad_h)
img_padded=Image.new("L",(new_w+pad_w,new_h+pad_h))
img_padded.paste(img_resized,(0,0))

transforms_bw=transforms.Compose([
    transforms.ToTensor()
])

bw_tensor=transforms_bw(img_padded).unsqueeze(0).to(device)

# output
with torch.no_grad():
    output=model(bw_tensor)

# usunięcie padding
output_cropped=output[:,:,:new_h,:new_w]

    
# konwersja tensora do obrazu PIL
output_image=transforms.ToPILImage()(output.squeeze(0).cpu().clamp(0,1))
output_image=output_image.resize(original_size, Image.BICUBIC)

output_dir=os.path.dirname(output_path)
os.makedirs(output_dir,exist_ok=True)
try:
    output_image.save(output_path)
except Exception as e:
    print(f"Bład podczas zapisu obrazu: {str(e)}", file=sys.stderr)
    sys.exit(1)