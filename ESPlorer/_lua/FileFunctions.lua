function FileToBytes(filename)
    file.open(filename)
    readstring = file.readline()
    print(readstring)
end