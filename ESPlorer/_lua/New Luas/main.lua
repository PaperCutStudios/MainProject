function handledata(socket, data)
    print("Receiving data: "..data)
    if(data:sub(1,1) == "0") then
        print("Getting playernum")
        local pnum = require("getavailplayer").getavailplayer()
        if(pnum~=0) then
            require("player"..pnum).connect(socket)
        end
        pnum=nil
    elseif(tonumber(data:sub(1,1))<=4) then
        if(data:sub(2,2) == "0") then
            require("player"..data:sub(1,1)).disconnect()
        elseif(data:sub(2,2) == "1") then
            socket:send("2" .. require("prefs").getSeed())
        elseif(data:sub(2,2) == "2") then
            require("player"..data:sub(1,1)).addanswer(data:sub(3))
        end
    end
end

cfg={}
cfg.ssid = "Conversity"
cfg.pwd = "Conversi"
wifi.ap.config(cfg)
cfg={
ip = "192.168.0.1",
netmask = "255.255.255.0",
gateway = "192.168.0.1"
}
wifi.ap.setip(cfg)
cfg=nil
require("startbutton").settrig()
local s=net.createServer(net.TCP,1200)
s:listen(80,function(c) c:on("receive", handledata) c:on("disconnection", function(skt,c)print("disconnect")end) end)
require("lights").off("blue")
