import json
import machine
import network
import urequests

led = machine.Pin(2, machine.Pin.OUT)

def do_connect():
    sta_if = network.WLAN(network.STA_IF)
    # sta_if.disconnect()
    led.value(0)
    if not sta_if.isconnected():
        print('connecting to network...')
        sta_if.active(True)
        sta_if.connect('IEEE', 'Ilovesolder')
        while not sta_if.isconnected():
            pass
    # print('network config:', sta_if.ifconfig())
    led.value(1)

do_connect()

# This doesn't seem to ever work
# ntptime.settime()

telemetry_point = {
    'timestamp': '2023-01-22T00:00:00Z',
    'accelerationX': 1,
    'accelerationY': 1,
    'accelerationZ': 1,
}
telemetry_point_json = json.dumps(telemetry_point)
# print(telemetry_point_json)

# response = urequests.get('http://jsonplaceholder.typicode.com/albums/1')
response = urequests.get('http://192.168.1.204:5283/swagger/v1/swagger.json')
print(response.text)
