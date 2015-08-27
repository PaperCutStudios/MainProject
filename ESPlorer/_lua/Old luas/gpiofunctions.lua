local gpiofunc = {};
function gpiofunc.InitAllLights()
    for i=1,4 do
         gpio.mode(i, gpio.OUTPUT)
         gpio.write(i, gpio.HIGH)
    end
end
function gpiofunc.InitAllButtons()
    for i=5,8 do
        gpio.mode(i,gpio.INT,gpio.PULLUP)
        print("button "..i.." initialised")
    end
end
function gpiofunc.LightOn(colour)
    local pin=1
    if colour == "green" then pin = 3
    elseif colour == "blue" then pin = 4
    elseif colour == "yellow" then pin = 2
    elseif colour == "red" then pin = 1
    end
    gpio.write(pin, gpio.LOW)
end
function gpiofunc.LightOff(colour)
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
return gpiofunc;
