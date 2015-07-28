require"FileFunctions"
--GPIO Functions
function InitLightGPIO(PinNum)
    gpio.mode(PinNum, gpio.OUTPUT, gpio.PULLUP)
    gpio.write(PinNum, gpio.HIGH)
end

function InitButtonGPIO(PinNum)
    gpio.mode(PinNum, gpio.INT)
end

function InitAllLights()
    for i=1,4 do
        InitLightGPIO(i)
    end
end

function InitAllButtons()
    for i=5,8 do
        InitButtonGPIO(i)
    end
end

function LightOn(colour)
    local pin=1
    if colour == "green" then pin = 3
    elseif colour == "blue" then pin = 4
    elseif colour == "yellow" then pin = 2
    elseif colour == "red" then pin = 1
    end
    gpio.write(pin, gpio.LOW)
end

function LightOff(colour)
    local pin=1
    if colour == "green" then pin = 3
        gpio.write(pin, gpio.HIGH)
    elseif colour == "blue" then pin = 4
        gpio.write(pin, gpio.HIGH)
    elseif colour == "yellow" then pin = 2
        gpio.write(pin, gpio.HIGH)
    elseif colour == "red" then pin = 1
        gpio.write(pin, gpio.HIGH)
    elseif colour == "all" then 
        InitAllLights()
    end
end
--Initialisations

InitAllLights()
InitAllButtons()

numPlayers = 0

player1 = {
	playerNum = 1,
	playerIP = "",
	playerAnswer = ""
}
player2 = {
	playerNum = 2,
	playerIP = "",
	playerAnswer = ""
}
player3 = {
	playerNum = 3,
	playerIP = "",
	playerAnswer = ""
}
player4 = {
	playerNum = 4,
	playerIP = "",
	playerAnswer = ""
}



wifi.setmode(wifi.SOFTAP)
apcfg = {}
apcfg.ssid = "PapercutGame"
apcfg.password = "password"
wifi.ap.config(apcfg)

ipcfg = {
    ip = "192.168.0.1",
    netmask = "255.255.255.0",
    gateway = "192.168.0.1"
}
wifi.ap.setip(ipcfg)


Server = net.createServer(net.TCP, 300)


--Networking Functions

function HandleSocketData (Socket,DataStream)
--if the first character in the DataStream string is a 0 (byte 48), we know that this socket connection is an noninitialised player
	funcIdentifier = "0"
    if(DataStream:byte(1) == string.byte("0")) then
        if(numPlayers == 0) then
            player1.playerIP = string.char(DataStream:byte(2,DataStream:len())))
			Socket:send(funcIdentifier .. player1[playerNum])
			LightOn("blue")
		elseif(numPlayers == 1) then
            player2.playerIP = string.char(DataStream:byte(2,DataStream:len())))
			Socket:send(funcIdentifier .. player2[playerNum])
			LightOn("green")
		elseif(numPlayers == 2) then
            player3.playerIP = string.char(DataStream:byte(2,DataStream:len())))
			Socket:send(funcIdentifier .. player3[playerNum])
			LightOn("yellow")
		elseif(numPlayers == 3) then
            player4.playerIP = string.char(DataStream:byte(2,DataStream:len())))
			Socket:send(funcIdentifier .. player4[playerNum])
			LightOn("red")
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

