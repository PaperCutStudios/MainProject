function HandleSocketData (Socket,DataStream)
print("receiving data")
    if(tonumber(DataStream:sub(1,1)) == 0) then
        local serverfuncs = require("ServerFuncs1")
        serverfuncs.InitialJoin(Socket)
    elseif(tonumber(DataStream:sub(1,1)) <= 4) then
        local player = require("player"..DataStream:sub(1,1))
        print("Player"..player.ID)
        if(tonumber(DataStream:sub(2,2)) == 0) then
            print ("disconnecting player"..player.ID)
            player.disconnect()
            local gamePrefs = require("gamePrefs")
            gamePrefs.RemovePlayer(player.ID)
        elseif(tonumber(DataStream:sub(2,2)) == 1) then
            print ("Sending seed to"..player.ID)
            local gamePrefs = require("gamePrefs")
            local seed = gamePrefs.GetSeed()
            print(seed)
            print(string.len(seed))
            player.Socket:send("2"..string.len(seed)..seed)
        end
    else
        print("Data not workable")       
    end
end
function OnConnection(Socket)
    Socket:on("receive", HandleSocketData)

end
--ServerListening
Server:listen(80,OnConnection)
