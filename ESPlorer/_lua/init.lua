--GPIO Functions
function InitLightGPIO(PinNum)
    gpio.mode(PinNum, gpio.OUTPUT, gpio.PULLUP)
    gpio.write(PinNum, gpio.HIGH)
end

function InitButtonGPIO(PinNum)
    gpio.mode(PinNum, gpio.INT)
end

function InitAllLights()
    for i=1,4 do
        InitLightGPIO(i)
    end
end

function InitAllButtons()
    for i=5,8 do
        InitButtonGPIO(i)
    end
end

function LightOn(colour)
    local pin=1
    if colour == "green" then pin = 3
    elseif colour == "blue" then pin = 4
    elseif colour == "yellow" then pin = 2
    elseif colour == "red" then pin = 1
    end
    gpio.write(pin, gpio.LOW)
end

function LightOff(colour)
    local pin=1
    if colour == "green" then pin = 3
        gpio.write(pin, gpio.HIGH)
    elseif colour == "blue" then pin = 4
        gpio.write(pin, gpio.HIGH)
    elseif colour == "yellow" then pin = 2
        gpio.write(pin, gpio.HIGH)
    elseif colour == "red" then pin = 1
        gpio.write(pin, gpio.HIGH)
    elseif colour == "all" then 
        InitAllLights()
    end
end


--Networking Functions

function PrintFromSocket (Socket,DataStream)
    print(DataStream)
end

function OnConnectionSuccess(Socket)
    Socket:on("receive", PrintFromSocket)
    Socket:send("Connection Initiated")
end

InitAllLights()
InitAllButtons()

wifi.setmode(wifi.SOFTAP)
apcfg = {}
apcfg.ssid = "PapercutGame"
apcfg.password = "password"
wifi.ap.config(apcfg)

ipcfg = {
    ip = "192.168.0.1",
    netmask = "255.255.255.0",
    gateway = "192.168.0.1"
}
wifi.ap.setip(ipcfg)


Server = net.createServer(net.TCP, 300)
Server:listen(80,OnConnectionSuccess)

