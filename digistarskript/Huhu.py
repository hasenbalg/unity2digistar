import time
import urllib
imgurl = 'http://127.0.0.1:73/'
imgurl = 'https://www.nordfront.se/wp-content/uploads/2018/01/blaljus-460x195.jpg'


def loop():
        start = time.time()
        response = urllib.urlopen(imgurl).read()
        print(response)
        urllib.urlretrieve(imgurl,'blaljus.jpg')
        print(1.0/(time.time() - start))

def main():
        while True:
                loop()


if __name__ == '__main__':
        main()
