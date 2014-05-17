﻿/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Fidely.Demo.GettingStarted.WPF.Diagnostics
{
    public class InMemoryTraceListener : TraceListener
    {
        public static event EventHandler<FlashedEventArgs> Flashed;


        private readonly StringBuilder messages;


        public InMemoryTraceListener()
        {
            messages = new StringBuilder();
        }


        public override void Flush()
        {
            base.Flush();

            if (Flashed != null)
            {
                Flashed(this, new FlashedEventArgs(messages.ToString().Split(new string[]{ Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList()));
            }

            messages.Clear();
        }

        public override void Write(string message)
        {
            messages.Append(message);
        }

        public override void WriteLine(string message)
        {
            messages.AppendLine(message);
        }
    }
}
