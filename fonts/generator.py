import os
import subprocess
import sys

l = os.listdir('.')
for f in l:
    if f[-4:] == '.ttf':
        os.system("mkdir " + f[:-4] + " 2>/dev/null")
        for i in range(256):
            line = "convert -background none -fill white -font " + f + " -pointsize 300 label:$'\\x" + hex(i)[2:] + "' " + f[:-4] + "/" + str(i) + ".png >/dev/null 2>/dev/null"
            os.system(line)
            print int((1.0 + i) / 512.0 * 100.0), "%\r",
            sys.stdout.flush();
        for i in range(2, 256):
            print int((256.0 + i) / 512.0 * 100.0), "%\r",
            sys.stdout.flush();
            if (subprocess.call("compare -metric rmse " + f[:-4] + "/1.png " + f[:-4] + "/" + str(i) + ".png null: >/dev/null 2>/dev/null", shell=True) == 0):
                os.system("rm " + f[:-4] + "/" + str(i) + ".png")
        os.system("rm " + f[:-4] + "/1.png")
