local lights={}
function lights.on(colour)
	local pin=1
    if colour == "green" then pin = 3
    elseif colour == "blue" then pin = 4
    elseif colour == "yellow" then pin = 2
    elseif colour == "red" then pin = 1
    end
    gpio.write(pin, gpio.LOW)
end
function lights.off(colour)
	local pin=1
    if colour == "green" then pin = 3
    elseif colour == "blue" then pin = 4
    elseif colour == "yellow" then pin = 2
    elseif colour == "red" then pin = 1
    end
    gpio.write(pin, gpio.HIGH)
end
return lights