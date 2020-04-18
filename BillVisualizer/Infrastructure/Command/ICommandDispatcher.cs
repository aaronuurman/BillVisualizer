using System.Threading.Tasks;

namespace BillVisualizer.Infrastructure.Command
{
  public interface ICommandDispatcher
  {
    Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
  }
}