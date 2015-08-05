local player1 = {}
player1.ID = 1;
player1.AnswerIDs = "A";
function player1.SetSocket(Socket)
    player1.Socket = Socket;
end
function player1.SetAnswerID(ID)
    player1.AnswerIDs = ID
end
function player1.GetAnswerID()
    return player1.AnswerIDs
end
return player1;
