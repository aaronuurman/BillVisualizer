using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BillVisualizer.Infrastructure.Command
{
  public class CommandDispatcher : ICommandDispatcher
  {
    public IServiceProvider Services { get; set; }

    public CommandDispatcher(IServiceProvider services)
    {
      Services = services;
    }

    public async Task Execute<TCommand>(TCommand command)
      where TCommand : ICommand
    {
      if (command == null)
      {
        throw new ArgumentNullException("Command can't be null.");
      }

      var handler = Services.GetRequiredService<ICommandHandler<TCommand>>();

      if (handler == null)
      {
        throw new ArgumentNullException("Handler can't be null.");
      }

      await handler.Handle(command);
    }
  }
}