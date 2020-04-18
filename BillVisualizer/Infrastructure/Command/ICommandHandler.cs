using System.Threading.Tasks;

namespace BillVisualizer.Infrastructure.Command
{
  public interface ICommandHandler<TCommand> where TCommand : ICommand
  {
    Task Handle(TCommand command);
  }
}