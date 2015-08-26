for i=1,8 do
if(i <= 4) then
gpio.mode(i,gpio.OUTPUT)
gpio.write(i,gpio.HIGH)
else
gpio.mode(i,gpio.INT,gpio.PULLUP)
gpio.write(i,gpio.HIGH)
end
end
require("lights").on("blue")
tmr.alarm(0,3000,0,function()dofile("main.lua")end)

	