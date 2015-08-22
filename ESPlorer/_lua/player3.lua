local player = {}
player.ID = 3;
player.AnswerIDs = " ";
player.Socket = ""
function player.connect(Socket)
    player.Socket = Socket;
    print("Player "..player.ID.." has Joined")
    local gpio = require("gpiofunctions")
    gpio.LightOn("yellow")
end
function player.disconnect()
    player.Socket:close()
    print("Player "..player.ID.." has Disconnected")
    local gpio = require("gpiofunctions")
    gpio.LightOff("yellow") 
end
function player.setAnswer(answer)
    print("Setting Player "..player.ID.."'s Answer to: ".. answer)
    player.AnswerIDs = answer
    player.Socket:send("7")
    local gameprefs = require("gamePrefs")
    gameprefs.AnswerAdded()
end
return player;
