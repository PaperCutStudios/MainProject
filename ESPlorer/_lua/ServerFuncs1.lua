local ServerFuncs1 = {}

function ServerFuncs1.InitialJoin (SocketPass)
    local gpiofunctions = require("gpiofunctions")
    local funcIdentifier = "0"
    if(numPlayers == 0) then
        local player = require("player1")
        player.SetSocket(SocketPass)
        gpiofunctions.LightOn("blue")
        SocketPass:send("0"..player.ID)
    elseif(numPlayers == 1) then
        local player = require("player2")
        player.SetSocket(SocketPass)
        gpiofunctions.LightOn("green")
        SocketPass:send("0"..player.ID)
    elseif(numPlayers == 2) then
        local player = require("player3")
        player.SetSocket(SocketPass)
        gpiofunctions.LightOn("yellow")
        SocketPass:send("0"..player.ID)
    elseif(numPlayers == 3) then
        local player = require("player4")
        player.SetSocket(SocketPass)
        gpiofunctions.LightOn("red")
        SocketPass:send("0"..player.ID)
    else
        SocketPass:send("0")
    end
    numPlayers = numPlayers + 1;
end

return ServerFuncs1
