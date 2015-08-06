local player3 = {}
player3.ID = 3;
player3.AnswerIDs = "";
function player3.SetSocket(Socket)
    player3.Socket = Socket;
    print("Player "..player3.ID.." has Joined")
end
return player3;
