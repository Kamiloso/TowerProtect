using System;

namespace TowerProtect.Domain.Model.Exceptions;

public class UnreachableException()
    : Exception("The unreachable code has been reached");
