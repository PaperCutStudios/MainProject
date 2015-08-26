cfg={}
cfg.ssid = "Conversity"
cfg.pwd = "Conversity"
wifi.ap.config(cfg)
cfg={
ip = "192.168.0.1",
netmask = "255.255.255.0",
gateway = "192.168.0.1"
}
wifi.ap.setip(cfg)
cfg=nil
local s=net.createServer(net.TCP,1200)
s:listen(80,function(c) c:on("receive", function(c,stm)require("handledata")(c,stm)end) end)
require("lights").off("blue")