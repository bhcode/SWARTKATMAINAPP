public enum HiveCol
{
    IDCol = 0, LocCol = 1, HiveBodyCol = 2, HoneySupCol = 3, FramesCol = 4, HiveSpeciesCol = 5, ForageCol = 6
}

enum UploadStage
{
    nil = 0,
    selectFile = 1,
    processFile = 2,
    uploadFile = 3,
    complete = 4
}

public enum PermissionLevel
{
    Guest = 2,
    User = 1,
    Admin = 0
}