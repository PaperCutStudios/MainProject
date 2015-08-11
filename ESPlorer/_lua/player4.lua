local player4 = {}
player4.ID = 4;
player4.AnswerIDs = "";
function player4.SetSocket(Socket)
    player4.Socket = Socket;
    print("Player "..player4.ID.." has Joined")
end
function player4.LightOn()
    local gpio = require("gpiofunctions")
    gpio.LightOn("red")
end
return player4;
