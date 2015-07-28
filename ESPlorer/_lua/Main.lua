function HandleSocketData (Socket,DataStream)
    if(DataStream:byte(1) == string.byte("0")) then
        local gpiofunctions = require("gpiofunctions")
        local funcIdentifier = "0"
        if(numPlayers == 0) then
            local player = require("player1")
            player.SetIP( string.char(DataStream:byte(2,DataStream:len())))
            gpiofunctions.LightOn("blue")
        elseif(numPlayers == 1) then
            local player = require("player2")
            player.SetIP( string.char(DataStream:byte(2,DataStream:len())))
            gpiofunctions.LightOn("green")
        elseif(numPlayers == 2) then
            local player = require("player3")
            player.SetIP( string.char(DataStream:byte(2,DataStream:len())))
            gpiofunctions.LightOn("yellow")
        elseif(numPlayers == 3) then
            local player = require("player4")
            player.SetIP( string.char(DataStream:byte(2,DataStream:len())))
            gpiofunctions.LightOn("red")
        end
        
        numPlayers = numPlayers + 1;
    end
end
function OnConnectionSuccess(Socket)
    Socket:on("receive", HandleSocketData)
    Socket:send("Connection Initiated")
end
--ServerListening
Server:listen(80,OnConnectionSuccess)