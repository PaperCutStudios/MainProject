local button = {}
function button.debounce(func)
    local last = 0
    local delay = 200000
    return function (...)
        local now = tmr.now()
        if now - last < delay then return end
        last = now
        return func(...)
    end
end
function button.togglegame(level)
    local pinnum = 5
    local prefs = require("prefs")
    print("ToggleGame")
    if(prefs.started == false) then
        for key,value in pairs(prefs.players) do
            local player = require("player"..value)
            player.socket:send("3")
            print ("GameStart sent to a player")
        end
        prefs.started = true
    else 
        for key,value in pairs(prefs.players) do
            local player = require("player"..value)
            player.socket:send("4");
            print ("GameEnd sent to a player")
        end
    end
end
function button.settrig()
    gpio.trig(5,"down",button.debounce(button.togglegame))
    gpio.write(5,gpio.HIGH)
end
return button