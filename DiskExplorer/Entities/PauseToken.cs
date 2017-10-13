﻿using System.Threading;
using System.Threading.Tasks;

namespace DiskExplorer.Entities
{
    public class PauseTokenSource {
        // https://blogs.msdn.microsoft.com/pfxteam/2013/01/13/cooperatively-pausing-async-methods/
        internal static readonly Task s_completedTask = Task.FromResult(true);
        private volatile TaskCompletionSource<bool> m_paused;

        public PauseToken Token { get { return new PauseToken(this); } }

        internal Task WaitWhilePausedAsync() {
            var cur = m_paused;
            return cur != null ? cur.Task : s_completedTask;
        }

        public bool IsPaused {
            get { return m_paused != null; }
            set {
                if (value) {
                    Interlocked.CompareExchange(ref m_paused, new TaskCompletionSource<bool>(), null);
                } else {
                    while (true) {
                        var tcs = m_paused;
                        if (tcs == null) return;
                        if (Interlocked.CompareExchange(ref m_paused, null, tcs) == tcs) {
                            tcs.SetResult(true);
                            break;
                        }
                    }
                }
            }
        }
    }

    public class PauseToken {
        private readonly PauseTokenSource m_source;

        internal PauseToken(PauseTokenSource source) {
            m_source = source;
        }

        public bool IsPaused { get { return m_source != null && m_source.IsPaused; } }

        public Task WaitWhilePausedAsync() {
            return IsPaused ? m_source.WaitWhilePausedAsync() : PauseTokenSource.s_completedTask;
        }
    }
}
