local player2 = {}
player2.ID = 2;
player2.AnswerIDs = "";
function player2.SetSocket(Socket)
    player2.Socket = Socket;
    print("Player "..player2.ID.." has Joined")
end
return player2;
