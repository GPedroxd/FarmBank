using FluentAssertions;
using System.Collections;

namespace FarmBank.Core.Tests.Event;

public class EventPropertyTestInlineDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    { };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class EventPropertyTest
{
      
}
