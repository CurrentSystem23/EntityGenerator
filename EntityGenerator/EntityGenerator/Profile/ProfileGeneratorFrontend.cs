﻿namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorFrontend"/> models generator settings for the frontend project.
  /// </summary>
  public class ProfileGeneratorFrontend
  {
    /// <summary>
    /// Flag for generating frontend constants.
    /// </summary>
    public bool Constants { get; set; }

    /// <summary>
    /// Flag for generating frontend viewmodels.
    /// </summary>
    public bool ViewModels { get; set; }
  }
}
