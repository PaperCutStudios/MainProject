function HandleSocketData (Socket,DataStream)
print("Receiving data: "..DataStream)
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
            print ("Player "..player.ID.." requesting game seed")
            local gamePrefs = require("gamePrefs")
            local seed = gamePrefs.GetSeed()
            player.Socket:send("2"..string.len(seed)..seed)
            print("Sent Seed: ".."2"..string.len(seed)..seed)
        elseif(tonumber(DataStream:sub(2,2) == 2)) then
            print ("Player "..player.ID.." sending their answer")
            player.setAnswer(DataSteam:sub(3))          
        end            
    else
        print("Data not workable")       
    end
end
function OnConnection(Socket)
    Socket:on("receive", HandleSocketData)

end

local buttonInit = require("ButtonFuncs")
buttonInit.SetButtonTrigs()
Server:listen(80,OnConnection)
local gpio = require("gpiofunctions")
gpio.LightOff("blue")
