local player = {}
player.ID = 3;
player.AnswerIDs = " ";
function player.connect(Socket)
    player.Socket = Socket;
    print("Player "..player.ID.." has Joined")
    local gpio = require("gpiofunctions")
    gpio.LightOn("yellow")
end
function player.disconnect()
    player.Socket:close
    print("Player "..player.ID.." has Disconnected")
    local gpio = require("gpiofunctions")
    gpio.LightOff("yellow") 
end
return player;