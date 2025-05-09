﻿using CommandLine;
using DockedBackup.Enums;

namespace DockedBackup.Models.Kopia;

[Verb("create", HelpText = "Establishing a connection to the repository")]
public class CreateFilesystem
{
    [Option('t', "type", HelpText = "Specifies the repository type (e.g., s3 = 0, filesystem = 1)", Required = true)]
    public required Provider Type { get; set; }
    
    [Option("path", HelpText = "Path to the new repository", Required = true)]
    public required string Path { get; set; }

    [Option('p', "password", HelpText = "Specifies the repository password", Required = true)]
    public required string Passwort { get; set; }
}