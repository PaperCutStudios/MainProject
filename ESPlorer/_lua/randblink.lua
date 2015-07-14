gpio.mode(4,gpio.INPUT)
currentlight=1
prevlight=0
tmr.alarm(0,1000,1,function()
    gpio.write(currentlight,gpio.LOW)
    gpio.write(prevlight,gpio.HIGH)
    randlight=math.random(1,4)
    while randlight==currentlight do
        randlight=math.random(1,4)
    end
    prevlight=currentlight
    currentlight=randlight
end)
    
    
    