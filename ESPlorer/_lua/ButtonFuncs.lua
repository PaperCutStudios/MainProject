local ButtonFuncs = {}
function ButtonFuncs.debounce(func)
    local last = 0
    local delay = 200000
    return function (...)
        local now = tmr.now()
        if now - last < delay then return end

        last = now
        return func(...)
    end
end
function ButtonFuncs.Difficulty(level)
    local pinnum = 5
    local gameprefs = require("gamePrefs")
    if(gameprefs.GameStarted ~=true) then
        if(gameprefs.Difficulty == 1 or gameprefs.Difficulty == 0) then
            gameprefs.Difficulty = gameprefs.Difficulty + 1
        else
            gameprefs.Difficulty = 0
        end
        print ("New difficulty is "..gameprefs.Difficulty)
        for key,value in pairs(gameprefs.Players) do
            local player = require("player"..value)
            player.Socket:send("5"..GamePrefs.Difficulty);
        end
    end
end
function ButtonFuncs.TimerUp(level)
    local pinnum = 7
    local gameprefs = require("gamePrefs")
    if(gameprefs.GameStarted ~=true) then
        if(gameprefs.Time <600) then
            gameprefs.Time = gameprefs.Time + 30
            print(gameprefs.Time)
            for key,value in pairs(gameprefs.Players) do
                local player = require("player"..value)
                player.Socket:send("6"..GamePrefs.Time);
            end
        end
    end
    
end
function ButtonFuncs.TimerDown(level)
    local pinnum = 6
    local gameprefs = require("gamePrefs")
    if(gameprefs.GameStarted ~=true) then
        if(gameprefs.Time > 120) then
            gameprefs.Time = gameprefs.Time - 30
            print(gameprefs.Time)
            for key,value in pairs(gameprefs.Players) do
                local player = require("player"..value)
                player.Socket:send("6"..GamePrefs.Time);
            end
        end
    end
end
function ButtonFuncs.GameStartEnd(level)
    local pinnum = 8
    local gameprefs = require("gamePrefs")
    for key,value in pairs(gameprefs.Players) do
        local player = require("player"..value)
        if(gameprefs.GameStarted == true) then
            player.Socket:send("3");
            print ("GameStart sent to player: "..value)
        else 
            player.Socket:send("4");
            print("GameEnd sent to player: "..value)
        end
    end
end
function ButtonFuncs.SetButtonTrigs()
    gpio.trig(5,"down", ButtonFuncs.debounce(ButtonFuncs.Difficulty))
    gpio.trig(6,"down", ButtonFuncs.debounce(ButtonFuncs.TimerDown))
    gpio.trig(7,"down", ButtonFuncs.debounce(ButtonFuncs.TimerUp))
    gpio.trig(8,"down", ButtonFuncs.debounce(ButtonFuncs.GameStartEnd))
    gpio.write(5, gpio.HIGH)
    gpio.write(6, gpio.HIGH)
    gpio.write(7, gpio.HIGH)
    gpio.write(8, gpio.HIGH)
end
return ButtonFuncs
