#!/usr/bin/python
import web
import RPi.GPIO as GPIO
import dht11
import time
import datetime

GPIO.setwarnings(True)
GPIO.setmode(GPIO.BCM)

instance = dht11.DHT11(pin=17)

urls = (
    '/', 'index'
)

class index:
    def GET(self):
        result = instance.read()
        if result.is_valid():
           print("got a request")
        resp = str(datetime.datetime.now())
        resp2 = str(result.temperature) + ',' + str(result.humidity) + ',' + str((result.temperature * 9/5) +32)
        return (resp + ',' + resp2)

if __name__ == "__main__":
    app = web.application(urls, globals())
    app.run()
