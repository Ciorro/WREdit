namespace WREdit.Base.Processing.Execution
{
    public class ExecutionStack
    {
        private readonly Stack<ProcessExecution> _history = new();

        public bool CanUndo => _history.Any();

        public async Task Execute(ProcessExecution execution, IProgress<ProgressReport> progress)
        {
            await execution.Execute(progress);
            _history.Push(execution);
        }

        public async Task Undo(IProgress<ProgressReport> progress)
        {
            if (_history.TryPop(out var exec))
            {
                await exec.Undo(progress);
            }
        }
    }
}
