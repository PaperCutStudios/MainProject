local ButtonFuncs = {}
function ButtonFuncs.Difficulty(level)
     print("Difficulty Cycle")
     --if level == 1 then gpio.trig(5, "down",  ButtonFuncs.Difficulty) else gpio.trig(5, "up", ButtonFuncs.Difficulty) end
end
function ButtonFuncs.TimerUp(level)
    local pinnum = 7
    print("TimerUp ")
    --if level == 1 then gpio.trig(pinnum, "down") else gpio.trig(pinnum, "up") end
end
function ButtonFuncs.TimerDown(level)
    local pinnum = 6
    print("TimerDown ")
    --if level == 1 then gpio.trig(pinnum, "down") else gpio.trig(pinnum, "up") end
end
function ButtonFuncs.GameStart(level)
    local pinnum = 8
    local gameprefs = require("gamePrefs")
    if(gameprefs.GameStarted ~=true) then
        local serverfuncs = require("GamePrefsServerFuncs")
        serverfuncs.SendGameStart()
        print("GameStarted ")
        gameprefs.GameStarted = true
    end
    --if level == 1 then gpio.trig(pinnum, "down", ButtonFuncs.GameStart) else gpio.trig(pinnum, "up",ButtonFuncs.GameStart) end
    print("Game already started!")
end
function ButtonFuncs.SetButtonTrigs()
    gpio.trig(5,"down", ButtonFuncs.Difficulty)
    gpio.trig(6,"down", ButtonFuncs.TimerDown)
    gpio.trig(7,"down", ButtonFuncs.TimerUp)
    gpio.trig(8,"down", ButtonFuncs.GameStart)
    gpio.write(5, gpio.HIGH)
    gpio.write(6, gpio.HIGH)
    gpio.write(7, gpio.HIGH)
    gpio.write(8, gpio.HIGH)
end
return ButtonFuncs
