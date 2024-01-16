using WREdit.Base.Entities;

namespace WREdit.Base.Processing.Execution
{
    public class ProcessExecution
    {
        private readonly IEntityProcessor _processor;
        private readonly IEnumerable<IEntity> _entities;
        private readonly Queue<(string File, string Content)> _changes;

        public ProcessExecution(IEntityProcessor processor, IEnumerable<IEntity> entities)
        {
            _processor = processor;
            _entities = entities;
            _changes = new Queue<(string, string)>(entities.Count());
        }

        public async Task Execute(IProgress<ProgressReport> progress)
        {
            await Task.Run(() =>
            {
                int i = 0;
                foreach (var entity in _entities)
                {
                    progress.Report(new ProgressReport
                    {
                        CurrentFile = entity.FileName,
                        Progress = (float)i++ / _entities.Count()
                    });

                    _changes.Enqueue((entity.FileName, entity.Content));
                    _processor?.Execute(entity);
                    entity.Save();
                }

                progress.Report(new ProgressReport());
            });
        }

        public async Task Undo(IProgress<ProgressReport> progress)
        {
            await Task.Run(() =>
            {
                int changesCount = _changes.Count;
                int changesReverted = 0;

                while (_changes.TryDequeue(out var change))
                {
                    progress.Report(new ProgressReport
                    {
                        CurrentFile = change.File,
                        Progress = (float)changesReverted++ / changesCount
                    });

                    try
                    {
                        File.WriteAllText(change.File, change.Content);
                    }
                    catch { }
                }

                progress.Report(new ProgressReport());
            });
        }
    }
}
