const gb = require("../gbtest")(__filename);

test("str-equals", () => {
  gb.run();
  expect(gb.stack).toEqual([0x0, 0x0, 0x0, 0x0, 0xffff]);
});
