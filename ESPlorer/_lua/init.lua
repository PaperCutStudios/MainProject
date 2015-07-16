function InitLightGPIO(PinNum)
    gpio.mode(PinNum, gpio.OUTPUT, gpio.PULLUP)
    gpio.write(PinNum, gpio.HIGH)
end

function InitButtonGPIO(PinNum)
    gpio.mode(PinNum, gpio.INT)
end

function InitAllGPIO()
    for i=1,4 do
        InitLightGPIO(i)
    end
    for i=5,8 do
        InitButtonGPIO(i)
    end
end

InitAllGPIO()

success = "Connected!"

wifi.setmode(wifi.SOFTAP)
cfg = {}
cfg.ssid = "PapercutGame"
cfg.password = "password"
wifi.ap.config(cfg)

function PrintFromSocket (Socket,DataStream)
    print(DataStream)
end

function OnConnectionSuccess(Socket)
    Socket:on("receive", PrintFromSocket)
    Socket:send("Connection Initiated")
end

Server = net.createServer(net.TCP, 300)
Server:listen(80,OnConnectionSuccess)

