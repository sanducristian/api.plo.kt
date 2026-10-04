"""Static consistency checks only. Does not compile C# or connect to MySQL."""
from pathlib import Path
import re
import xml.etree.ElementTree as ET

root = Path(__file__).resolve().parents[1]
text = (root / 'docs/SCHEMA_EXCERPTS.md').read_text()
tables = {m.group(1): set(re.findall(r'^  `([^`]+)` ', m.group(2), re.M))
          for m in re.finditer(r'CREATE TABLE `([^`]+)` \((.*?)\n\) ENGINE=', text, re.S)}
ET.parse(root / 'Kt.Data.csproj')
projection_count = 0

for line in (root / 'Objects/KtObjectTable.cs').read_text().splitlines():
    if line.strip().startswith('new("'):
        name, *columns = re.findall(r'"([^"]+)"', line)
        assert name in tables, name
        assert set(columns) <= tables[name], (name, set(columns) - tables[name])
        projection_count += 1
insert_count = 0

for file in (root / 'Repositories').glob('*.cs'):
    source = file.read_text()
    for match in re.finditer(r'INSERT INTO (\w+)\s*\(([^)]+)\)', source):
        table, columns = match.groups()
        names = {c.strip().strip('`') for c in columns.split(',')}
        assert table in tables, table
        assert names <= tables[table], (file.name, table, names - tables[table])
        insert_count += 1
assert projection_count == 26
assert insert_count >= 5
print(f'PASS: {projection_count} concrete projections and {insert_count} INSERT column sets match source DDL.')
print('Compilation, parameter binding, SQL execution and transactional behaviour remain unverified.')
