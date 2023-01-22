import json
import machine
import network
import urequests
import ujson
# import time

led = machine.Pin(2, machine.Pin.OUT)

# while True:
#     led.value(1)
#     time.sleep(1)
#     led.value(0)
#     time.sleep(1)


def do_connect():
    sta_if = network.WLAN(network.STA_IF)
    # sta_if.disconnect()
    if not sta_if.isconnected():
        led.value(0)
        print('connecting to network...')
        sta_if.active(True)
        sta_if.connect('IEEE', 'Ilovesolder')
        while not sta_if.isconnected():
            pass
    # print('network config:', sta_if.ifconfig())
    led.value(1)


do_connect()

telemetry_points = []


def accumulate_point():
    global telemetry_points
    telemetry_point = {
        'timestamp': 12345,
        'accelerationX': 1,
        'accelerationY': 1,
        'accelerationZ': 1,
    }
    telemetry_points.append(telemetry_point)


pending_posts = []


def accumulate_catch():
    global telemetry_points
    global pending_posts
    catch_event = {
        'timestamp': 12345,
        'telemetryPoints': telemetry_points
    }
    pending_posts.append(catch_event)
    telemetry_points = []


accumulate_point()
accumulate_point()
accumulate_point()
accumulate_catch()

accumulate_point()
accumulate_point()
accumulate_catch()

# response = urequests.get('http://jsonplaceholder.typicode.com/albums/1')
# response = urequests.get('http://192.168.1.204:5283/swagger/v1/swagger.json')
headers = {'accept': '*/*'}
data = ujson.dumps(pending_posts)
response = urequests.post(
    'http://192.168.1.204:5283/api/catch-events',
    # headers=headers,
    # data=''
)
print(response.json())
response.close()
