function HandleSocketData (Socket,DataStream)
    if(DataStream:byte(1) == string.byte("0")) then
        local serverfuncs = require("ServerFuncs1")
        serverfuncs.InitialJoin(Socket)
    elseif(DataStream:byte(2) == string.byte("0")) then
        --Disconnect the player
    elseif(DataSteam:byte(2)  == string.byte("1")) then
        local player = require("player"..DataStream:byte(1))
        local gamePrefs = require("gamePrefs")
        local seed = gamePrefs.GetSeed()
        print(seed)
        print(string.len(seed))
        player.Socket:send(string.len(seed)..seed)
           
    end
end
function OnConnection(Socket)
    Socket:on("receive", HandleSocketData)

end
--ServerListening
Server:listen(80,OnConnection)
