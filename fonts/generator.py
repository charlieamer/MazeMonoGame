import os

l = os.listdir('.')
for f in l:
    if f[-4:] == '.ttf':
        os.system("mkdir " + f[:-4])
        for i in range(256):
            line = "convert -background none -fill white -font " + f + " -pointsize 300 label:$'\\x" + hex(i)[2:] + "' " + f[:-4] + "/" + str(i) + ".png"
            os.system(line)
