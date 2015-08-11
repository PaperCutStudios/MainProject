local player1 = {}
player1.ID = 1;
player1.AnswerIDs = "A";
function player1.SetSocket(Socket)
    player1.Socket = Socket;
    print("Player "..player1.ID.." has Joined")
end
function player4.LightOn()
    local gpio = require("gpiofunctions")
    gpio.LightOn("red")
end
return player1;
