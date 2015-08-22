local player = {}
player.ID = 4;
player.AnswerIDs = " ";
player.Socket = ""
function player.connect(Socket)
    player.Socket = Socket;
    print("Player "..player.ID.." has Joined")
    local gpio = require("gpiofunctions")
    gpio.LightOn("red")
end
function player.disconnect()
    player.Socket:close()
    print("Player "..player.ID.." has Disconnected")
    local gpio = require("gpiofunctions")
    gpio.LightOff("red") 
end
function player.setAnswer(answer)
    print("Setting Player "..player.ID.."'s Answer to: ".. answer)
    player.AnswerIDs = answer
    local gameprefs = require("gamePrefs")
    gameprefs.AnswerAdded()
end
return player;
