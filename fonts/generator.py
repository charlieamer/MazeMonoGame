import os
import subprocess
import sys
import re

l = os.listdir('.')
for f in l:
    if f[-4:] == '.ttf':
        n = f[:-4]
        os.system("mkdir " + n + " 2>/dev/null")
        for i in range(256):
            line = "convert -background none -fill white -font " + f + " -pointsize 100 label:$'\\x" + hex(i)[2:] + "' " + n + "/" + str(i) + ".png >/dev/null 2>/dev/null"
            os.system(line)
            print int((1.0 + i) / 512.0 * 100.0), "%\r",
            sys.stdout.flush();
        for i in range(2, 256):
            print int((256.0 + i) / 512.0 * 100.0), "%\r",
            sys.stdout.flush();
            if (subprocess.call("compare -metric rmse " + n + "/1.png " + n + "/" + str(i) + ".png null: >/dev/null 2>/dev/null", shell=True) == 0):
                os.system("rm " + f[:-4] + "/" + str(i) + ".png")
        os.system("rm " + n + "/1.png")
        o = subprocess.check_output("TexturePacker --data Assets/" + n + "-{n}.xml --format xml --sheet Assets/" + n + "-{n}.png --disable-rotation --max-width 512 --max-height 512 --trim-mode None --multipack --force-publish " + n, shell = True)
        files = re.findall("Writing (.+\\.xml)", o)
        f = open("Assets/" + n + ".xml","w")
        f.write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<Includes>\n")
        for fname in files:
            f.write("  <include file=\"" + fname + "\"/>\n")
        f.write("</Includes>")
        f.close()
        print o
