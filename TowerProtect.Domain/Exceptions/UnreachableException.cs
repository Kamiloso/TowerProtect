using System;

namespace TowerProtect.Domain.Exceptions;

public class UnreachableException()
    : Exception("The unreachable code has been reached");
