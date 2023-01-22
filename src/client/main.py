import json
import machine
import network
import urequests
import ujson
import time
import math

def do_connect():
    global BLUE_LED
    sta_if = network.WLAN(network.STA_IF)
    # sta_if.disconnect()
    if not sta_if.isconnected():
        BLUE_LED.value(0)
        print('connecting to network...')
        sta_if.active(True)
        sta_if.connect('IEEE', 'Ilovesolder')
        while not sta_if.isconnected():
            pass
    print('network config:', sta_if.ifconfig())
    print("Connected!")
    BLUE_LED.value(1)

def accumulate_point():
    global telemetry_points
    global Ax_Pin, Ay_Pin, Az_Pin
    global scaleValue
    ticks = time.ticks_ms()
    Ax = Ax_Pin.read() * scaleValue
    Ay = Ay_Pin.read() * scaleValue
    Az = Az_Pin.read() * scaleValue
    telemetry_point = {
        'timestamp': ticks,
        'accelerationX': Ax,
        'accelerationY': Ay,
        'accelerationZ': Az,
    }
    telemetry_points.append(telemetry_point)

def check_catch():
    global telemetry_points
    # As a temporary check, each catch always consists of 50 points
    print(len(telemetry_points))
    if (len(telemetry_points) >= 50):
        return True
    return False

def accumulate_catch():
    global telemetry_points
    global pending_posts
    global LED_state
    catch_event = {
        'timestamp': telemetry_points[-1]["timestamp"],
        'telemetryPoints': telemetry_points
    }
    pending_posts.append(catch_event)
    telemetry_points = []
    LED_state += 1

def write_json(dictionary):
    global fileNumber
    # Serializing json
    json_object = json.dumps(dictionary)
    # Writing to sample.json
    with open("data" + str(fileNumber) + ".json", "w") as outfile:
        outfile.write("[" + json_object + "]")
    fileNumber += 1
    print("Wrote File.")

def post_data():
    global pending_posts
    if (len(pending_posts) == 0):
        return
    print("Timestamp: " + str(pending_posts[0]["timestamp"]))
    print(pending_posts[0])
    write_json(pending_posts[0])
    '''
    headers = {'content-type': 'application/json'}
    do_connect()
    data = ujson.dumps(pending_posts)
    response = urequests.post(
        'http://192.168.1.204:5283/api/catch-events',
        headers=headers,
        data=data)
    response.close()
    '''
    pending_posts = []
    print("Posted Data.")

'''
Ax_Pin = machine.ADC(machine.Pin(15))
Ay_Pin = machine.ADC(machine.Pin(2))
Az_Pin = machine.ADC(machine.Pin(4))
Ax_Pin.atten(machine.ADC.ATTN_11DB)
Ay_Pin.atten(machine.ADC.ATTN_11DB)
Az_Pin.atten(machine.ADC.ATTN_11DB)
'''
loop = True
while loop:
    try:
        Ax_Pin = machine.ADC(machine.Pin(15), atten=machine.ADC.ATTN_11DB)
        Ay_Pin = machine.ADC(machine.Pin(2), atten=machine.ADC.ATTN_11DB)
        Az_Pin = machine.ADC(machine.Pin(4), atten=machine.ADC.ATTN_11DB)
        loop = False
    except ValueError:
        pass
print("Configured Analog Read Pins")

fileNumber = 0

scaleValue = 3.0/4095.0 # 3 g max range

BLUE_LED = machine.Pin(2, machine.Pin.OUT)

frequency = 5000
PWM_OUT_1 = machine.PWM(machine.Pin(13), frequency)
PWM_OUT_2 = machine.PWM(machine.Pin(12), frequency)
PWM_OUT_3 = machine.PWM(machine.Pin(14), frequency)

Duty1 = 512
Duty2 = 512
Duty3 = 512
PWM_OUT_1.duty(Duty1)
PWM_OUT_2.duty(Duty2)
PWM_OUT_3.duty(Duty3)

#do_connect()

telemetry_points = []
pending_posts = []

measurement_interval = 50
LED_state = 0

ticks_initial = time.ticks_ms()
while True:
    PWM_OUT_1.duty(Duty1)
    PWM_OUT_2.duty(Duty2)
    PWM_OUT_3.duty(Duty3)
    ticks_now = time.ticks_ms()
    diff_time = time.ticks_diff(ticks_now, ticks_initial)
    if (diff_time >= measurement_interval):
        accumulate_point()
        if (check_catch()):
            accumulate_catch()
        post_data()
        
        #Duty1 = int(1023*len(telemetry_points)/100)
        #Duty2 = int(1023*len(telemetry_points)/100)
        #Duty3 = int(1023*len(telemetry_points)/100)
        Duty1 = int(511.5*(1+math.sin(6.28318530718*len(telemetry_points)/100)))
        Duty2 = int(511.5*(1+math.sin(6.28318530718*len(telemetry_points)/100)))
        Duty3 = int(511.5*(1+math.sin(6.28318530718*len(telemetry_points)/100)))

        BLUE_LED.value(LED_state % 2)
        ticks_initial = ticks_now
        LED_state = 0
