class Foo(dict):
  def __getitem__(self, key):
    print(key)
    return key

x = Foo()
print(x['hello'])
print(x.get('hello'))
