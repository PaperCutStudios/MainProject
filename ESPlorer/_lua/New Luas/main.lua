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
print(3 .. 3)
local s=net.createServer(net.TCP,1200)
print(node.heap())
s:listen(80,function(c) c:on("receive", function(c,stm)print(node.heap()) require("handledata")(c,stm)end) end)
require("lights").off("blue")
