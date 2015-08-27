--Initialisations
for i=1,8 do
if(i <= 4) then
gpio.mode(i,gpio.OUTPUT)
gpio.write(i,gpio.HIGH)
else
gpio.mode(i,gpio.INT,gpio.PULLUP)
gpio.write(i,gpio.HIGH)
end
end
apcfg = {}
apcfg.ssid = "Conversity"
apcfg.pwd = "Conversi"
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
require("lights").on("blue")
tmr.alarm(0,3000,0, function() dofile("Main.lua")end)
