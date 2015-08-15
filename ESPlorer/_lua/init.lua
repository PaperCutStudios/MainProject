--Initialisations
local gpiofunctions = require("gpiofunctions");
gpiofunctions.InitAllLights()
gpiofunctions.InitAllButtons()
if(wifi.getmode() ~= 2) then
    apcfg = {}
    apcfg.ssid = "PapercutGame"
    apcfg.password = "password"
    wifi.ap.config(apcfg)
    apcfg = nil
    ipcfg = {
        ip = "192.168.0.1",
        netmask = "255.255.255.0",
        gateway = "192.168.0.1"
    }
    wifi.ap.setip(ipcfg)
    ipcfg = nil
    Server = net.createServer(net.TCP, 300)
end

gpiofunctions.LightOn("blue")
tmr.alarm(0,1000,0,dofile("Main.lua"))
gpiofunctions.LightOff("blue")

