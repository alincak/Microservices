﻿namespace FreeCource.API.Basket.Settings
{
  public interface IRedisSettings
  {
    public string Host { get; set; }
    public int Port { get; set; }
  }
}
