--Initialisations
local gpiofunctions = require("gpiofunctions");
gpiofunctions.InitAllLights()
gpiofunctions.InitAllButtons()
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
Server = net.createServer(net.TCP, 1200)
gpiofunctions.LightOn("blue")
tmr.alarm(0,3000,0, function() dofile("Main.lua")end)
