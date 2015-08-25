function HandleSocketData (Socket,DataStream)
print("Receiving data: "..DataStream)
    if(DataStream:sub(1,1) == "0") then
        print("A player is requesting to join")
        local gamePrefs = require("gamePrefs")
        local playernum = gamePrefs.GetAvailablePlayer()
        print("GamePrefs is returning: "..playernum)
        if(playernum == 0) then
            print("Sending 0 as playernum")
            SocketPass:send("0")
        else
            print("Attemping to give player number")
            local player = require("player"..playernum)
            player.connect(Socket)
            player.Socket:send("1"..player.ID..gamePrefs.Difficulty..gamePrefs.Time)
            table.insert(gamePrefs.Players, player.ID)
        end
    elseif(tonumber(DataStream:sub(1,1)) <= 4) then
        local player = require("player"..DataStream:sub(1,1))
        print("DataStream[2] = "..DataStream:sub(2,2))
        if(DataStream:sub(2,2) == "0") then
            print ("disconnecting player"..player.ID)
            player.disconnect()
            local gamePrefs = require("gamePrefs")
            gamePrefs.RemovePlayer(player.ID)
        elseif(DataStream:sub(2,2) == "1") then
            print ("Player "..player.ID.." requesting game seed")
            local gamePrefs = require("gamePrefs")
            local seed = gamePrefs.GetSeed()
            player.Socket:send("2"..string.len(seed)..seed)
            print("Sent Seed: ".."2"..string.len(seed)..seed)
        elseif((DataStream:sub(2,2) == "2")) then
            print ("Player "..player.ID.." sending their answer")
            player.setAnswer(DataStream:sub(3))          
        end            
    else
        print("Data not workable")       
    end
end
function OnConnection(Socket)
    Socket:on("receive", HandleSocketData)
    Socket:on("disconnection", function(sck, c) print("Disconnect") end)
end

local buttonInit = require("ButtonFuncs")
buttonInit.SetButtonTrigs()
Server:listen(80,OnConnection)
local gpio = require("gpiofunctions")
gpio.LightOff("blue")
